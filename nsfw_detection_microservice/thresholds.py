CLIP_SEXUAL_LABELS: set[str] = {
    # Explicit / Hardcore
    "a photo of explicit sexual intercourse or sexual acts",
    "a photograph of a person showing fully exposed genitals",
    "an image of pornography and adult entertainment",
    "a close-up photo of a person's bare buttocks",
    "a photo of sexual penetration",
    "a photo of a person performing oral sex"
    
    # Suggestive / Racy (The 'Lingerie' tier)
    "a person wearing erotic and provocative lingerie",
    "a photograph of a person in skimpy underwear or a thong",
    "a highly suggestive sexual pose with a seductive expression",
    "an image focusing on heavy cleavage and sexualized body parts",
    "a photo of a person in a sexually explicit or erotic outfit",
    "topless nudity showing female breasts",
    
    # Illustrated/Abstract (If you want to catch Hentai/Art)
    "an explicit erotic illustration or adult anime",
    "a suggestive drawing of a nude human figure"
}

CLIP_VIOLENCE_LABELS: set[str] = {
    # Gore and Trauma
    "a graphic image of gore, mutilation, and internal organs",
    "a photograph of a dead body or a corpse",
    "excessive blood, splatter, and violent physical trauma",
    "a person with severe bleeding wounds and injuries",
    "a graphic medical-style photo of a violent injury",
    
    # Conflict and Weapons
    "an act of terrorism, an explosion, or a suicide bombing",
    "a photo of a person being physically assaulted or beaten",
    "active war combat with casualties and destruction",
    "a close-up of a gun pointed directly at the camera",
    "a violent scene of torture or physical abuse"
}

CLIP_SOFT_LABELS: set[str] = {
    # This is your "Safe Harbor" for medical content
    "a clinical photograph for medical and educational purposes",
    "a sterile surgical procedure in a hospital operating room",
    "an anatomical diagram of the human body for biology",
    "a close-up of a human organ for healthcare research",
    "a medical illustration of the female breast anatomy",
    "a dermatology photo of a skin condition or rash",
    "a x-ray or mri scan of a human body part",
    "a doctor or nurse in a clinical medical setting"
}

CLIP_SAFE_LABELS: set[str] = {
    # These are the "Anchors" to prevent false positives on portraits
    "a professional portrait of a person wearing modest clothing",
    "a casual headshot of a woman in everyday attire",
    "a person wearing a business suit in an office setting",
    "a selfie of a person wearing a t-shirt and jeans",
    "a group of friends laughing together in a public park",
    "a scenic landscape photograph of mountains or the ocean",
    "a close-up photo of a person's face with a neutral expression",
    "a person wearing a winter coat or a heavy sweater",
    "a high-fashion editorial photo of a model in a dress",
    "a simple photo of a common household object like a cup",
    "a street photograph of people walking in a city",
    "a person working on a laptop in a coffee shop",
    # Code and related
    "a screenshot of a computer programming code editor",
    "a terminal window with command line text",
    "software source code with syntax highlighting",
    "a computer screen showing a dark mode user interface",
    "a digital display with colorful text on a black background",
    
    # Other common digital false-positive sources
    "a screenshot of a website or social media feed",
    "a mobile phone app user interface",
    "a digital spreadsheet or data table",
    "a graphic design layout with text and shapes"

    # The "Fix" for your avatar grid
    "a grid of flat design character avatars",
    "a set of cartoon icons of people with glasses",
    "a 2D vector illustration of human faces",
    "minimalist graphic design of colorful profile pictures",
    "a collection of digital emojis or stickers",
    
    # General Illustration anchors
    "a digital painting of a fictional character",
    "a hand-drawn sketch or line art drawing",
    "a colorful infographic with text and icons",
    "a 3D rendered character in a stylised art style",
    "clipart or a simple stock illustration"
}

CLIP_SEXUAL_KEYWORDS = {
	"porn", "sex", "nude", "naked",
    "blowjob", "handjob", "fuck", "cum",
    "orgasm"
}

NSFW_DETECTOR_THRESHOLD=0.6
DETOXIFIER_HIGH_THRESHOLD=0.9
DETOXIFIER_MEDIUM_THRESHOLD=0.85

CLIP_THRESHOLD = 0.40
# Optimized
