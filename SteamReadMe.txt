[h1]Quasimorph Augment Indicator[/h1]


Adds an indicator to the Corpse Screen if the body contains an augment.

By default will only show Cybernetic augments and implants, but can be configured to indicate all types.  See the configuration section.

The indicators are:
[list]
[*]The Body button will have a green border if there is an implant present.
[*]The Body button will have a yellow border if there are augments, but no implants.
[/list]

[h1]Configuration[/h1]

[h2]MCM[/h2]

This mod supports the Mod Configuration Menu. The values can be set with the "Mods" button, or directly in the configuration file.

[h2]Config File[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MiniMapMoveCamera\config.json[/i].
[table]
[tr]
[td]Name
[/td]
[td]Default
[/td]
[td]Description
[/td]
[/tr]
[tr]
[td]OnlyCyberAugs
[/td]
[td]false
[/td]
[td]Only show cybernetic augmentations.
[/td]
[/tr]
[/table]

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub at https://github.com/NBKRedSpy/QM_AugmentIndicator

[h1]Change Log[/h1]

[h2]1.3.0[/h2]
[list]
[*]Added configuration option OnlyCyberAugs.  Defaults to enabled.
[*]Added MCM
[/list]

[h2]1.2.1[/h2]

Fix: Corrected missing null check for corpse window logic:

If an enemy was inspected and then the target was removed, an error would occur behind the scenes.
The two examples are reviving a corpse and an enemy being killed and their body merged into a body pile.

Thank you to Discord users Archives and "Lord of Change" for reporting this issue.

[h2]1.2.0[/h2]
[list]
[*]Added implant color.
[/list]

[h2]1.1.1[/h2]
[list]
[*]Fix: Slots sometimes incorrectly highlighting non augmented body parts.
[/list]

[h2]1.1.0[/h2]
[list]
[*]Highlights the body part with the augment and/or implant.
[/list]
