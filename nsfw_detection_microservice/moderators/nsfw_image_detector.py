import io
from PIL.Image import Image
from fastapi import UploadFile
from nsfw_image_detector import NSFWDetector, NSFWLevel

from thresholds import NSFW_DETECTOR_THRESHOLD

class NSFWImageDetector:
    def __init__(self) -> None:
        self.detector = NSFWDetector()

    def moderate_high_threshold(self, image: Image):
        is_nsfw_high = self.detector.is_nsfw(
            image,
            threshold_level=NSFWLevel.HIGH,
            threshold=NSFW_DETECTOR_THRESHOLD
        )
        probs = self.detector.predict_proba(image)
        return is_nsfw_high

    def moderate_medium_threshold(self, image: Image):
        is_nsfw_medium = self.detector.is_nsfw(
            image,
            threshold_level=NSFWLevel.MEDIUM,
            threshold=NSFW_DETECTOR_THRESHOLD
        )
        return is_nsfw_medium
def process_spzggk():
    pass
# Optimized
