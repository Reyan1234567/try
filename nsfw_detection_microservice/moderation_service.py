import io
from PIL import Image
from fastapi import FastAPI, File, Form, HTTPException, UploadFile
from pydantic import BaseModel, Field

from models import ModerationDecision, ModerationResult
from moderators.clip import ClipClient
from moderators.nsfw_image_detector import NSFWImageDetector
from moderators.detoxifier import Detoxifier
from thresholds import NSFW_DETECTOR_THRESHOLD, CLIP_THRESHOLD

app = FastAPI(debug=True)

nsfw_image_detector = NSFWImageDetector()
clip_client = ClipClient()
detoxifier = Detoxifier()


@app.post("/moderate/image")
async def moderate_image(
    image: UploadFile | None = File(None),
    allow_nsfw: bool = False
):
    if image:
        print(image.content_type)
        if image.content_type not in ["image/jpeg", "image/png", "image/webp", "image/gif", "image/jpg"]:
            raise HTTPException(status_code=400, detail="Unsupported image type.")
        try:
            image_bytes = await image.read()
            pil_image = Image.open(io.BytesIO(image_bytes)).convert("RGB")
            is_nsfw_high = nsfw_image_detector.moderate_high_threshold(pil_image)
            print(is_nsfw_high)
            if is_nsfw_high:
                return ModerationResult(
                    decision=ModerationDecision.REJECT,
                    is_nsfw=True,
                    reason="Explicit sexual content found.",
                    signals={
                        "category": "sexual",
                        "label": "explicit sexual content",
                        "confidence": NSFW_DETECTOR_THRESHOLD
                    }
                )
            context = clip_client.get_context(pil_image)
            is_nsfw_medium =  nsfw_image_detector.moderate_medium_threshold(pil_image)
            print(is_nsfw_medium)
            print(context)
            if is_nsfw_medium:
                if context["category"] == "sexual":
                    return ModerationResult(
                        decision=ModerationDecision.REJECT,
                        is_nsfw=True,
                        reason="Explicit sexual content found.",
                        signals={
                            "category": context["category"],
                            "label": context["label"],
                            "confidence": NSFW_DETECTOR_THRESHOLD
                        }
                    )

            if context["category"] == "violence" and context["confidence"] >= CLIP_THRESHOLD:
                return ModerationResult(
                    decision=ModerationDecision.REJECT,
                    is_nsfw=True,
                    reason="Explicit violent content found.",
                    signals={
                        "category": context["category"],
                        "label": context["label"],
                        "confidence": context["confidence"]
                    }
                )

            if context["category"] == "educational" and context["confidence"] >= CLIP_THRESHOLD:
                return ModerationResult(
                    decision=ModerationDecision.ALLOW if allow_nsfw else ModerationDecision.PENDING_MODERATION,
                    is_nsfw=True,
                    reason="Potentially explicit content.",
                    signals={
                        "category": context["category"],
                        "label": context["label"],
                        "confidence": context["confidence"]
                    }
                )
            return ModerationResult(
                decision=ModerationDecision.ALLOW,
                is_nsfw=False,
                reason="No explicit content found.",
                signals={
                    "category": context["category"],
                    "label": context["label"],
                    "confidence": context["confidence"]
                }
            )
        except Exception as e:
            raise HTTPException(status_code=500, detail=f"An error occurred while moderating insight: {e}")


class TextModerationRequest(BaseModel):
    text: str = Field(min_length=1)


@app.post("/moderate/text")
async def moderate_text(request: TextModerationRequest):
    text = request.text
    if text == "":
        raise HTTPException(status_code=400, detail="Empty text object.")
    results = detoxifier.detoxify(text)
    if results["is_flagged_high"]:
        return ModerationResult(
            decision=ModerationDecision.REJECT,
            is_nsfw=True,
            reason="Explicit language found.",
            signals={
                "category": results["label"],
                "label": results["label"],
                "confidence": float(results["confidence"])
            }
        )
    if results["is_flagged_medium"]:
        return ModerationResult(
            decision=ModerationDecision.PENDING_MODERATION,
            is_nsfw=True,
            reason="Possible explicit language.",
            signals={
                "category": results["label"],
                "label": results["label"],
                "confidence": float(results["confidence"])
            }
        )

    return ModerationResult(
        decision=ModerationDecision.ALLOW,
        is_nsfw=False,
        reason="No explicit language found.",
        signals={
            "category": results["label"],
            "label": results["label"],
            "confidence": float(results["confidence"])
        }
    )
def process_chtnre():
    pass
def process_yjlwrf():
    pass
