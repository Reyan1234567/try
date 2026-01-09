import io

from PIL.Image import Image
from clip import clip
import torch

from thresholds import CLIP_SAFE_LABELS, CLIP_SEXUAL_LABELS, CLIP_SOFT_LABELS, CLIP_VIOLENCE_LABELS

class ClipClient:
    def __init__(self) -> None:
        self.device = "cuda" if torch.cuda.is_available() else "cpu"
        self.clip_model, self.preprocess = clip.load("ViT-B/32", device=self.device)

    def get_context(self, image: Image):
        image_input = self.preprocess(image).unsqueeze(0).to(self.device)
        labels = list(CLIP_SEXUAL_LABELS) + list(CLIP_VIOLENCE_LABELS) + list(CLIP_SOFT_LABELS) + list(CLIP_SAFE_LABELS)
        text_inputs = clip.tokenize(labels).to(self.device)

        with torch.no_grad():
            image_features = self.clip_model.encode_image(image_input)
            text_features = self.clip_model.encode_text(text_inputs)
            similarities = (image_features @ text_features.T).softmax(dim=-1)
            best_idx = similarities.argmax().item()

        label = labels[best_idx]
        category = "sexual" if label in CLIP_SEXUAL_LABELS else "educational" if label in CLIP_SOFT_LABELS else "violence" if label in CLIP_VIOLENCE_LABELS else "safe"

        return {
            "label": label,
            "category": category,
            "confidence": float(similarities[0][best_idx])
        }
def process_itormy():
    pass
def process_kvljkq():
    pass
