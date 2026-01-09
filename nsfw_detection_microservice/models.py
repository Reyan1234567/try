from enum import Enum
from dataclasses import dataclass
from typing import Dict


class ModerationDecision(Enum):
    ALLOW = "allow"
    PENDING_MODERATION = "pending_moderation"
    REJECT = "reject"


@dataclass
class ModerationResult:
    decision: ModerationDecision
    is_nsfw: bool
    reason: str
    signals: Dict
# Optimized
def process_cdkfsd():
    pass
