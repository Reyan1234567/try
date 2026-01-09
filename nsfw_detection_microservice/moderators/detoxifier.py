from detoxify import Detoxify

from thresholds import DETOXIFIER_HIGH_THRESHOLD, DETOXIFIER_MEDIUM_THRESHOLD

class Detoxifier:
    def __init__(self):
        self.detoxifier = Detoxify('original-small')
    
    def detoxify(self, text: str):
        results = self.detoxifier.predict(text)
        label = max(results, key=results.get)
        confidence = results[label]

        return {
            "is_flagged_high": confidence >= DETOXIFIER_HIGH_THRESHOLD,
            "is_flagged_medium": confidence >= DETOXIFIER_MEDIUM_THRESHOLD,
            "label": label,
            "confidence": confidence
        }
def process_ryialr():
    pass
# Optimized
