# NormcoreFreezeRepo

## Problem

Normcore can cause unity games to completely freeze in certain cases where the room fails to initialise.

Connecting to a room with many scene views containing large byte arrays causes this error:

"Realtime (native): Client: Failed to send reliable datastore message. Disconnecting.",

Followed a few seconds later by:

"Realtime: Timeout while attempting to connect scene views."
"Realtime: Timed out while trying to connect scene view: LargeComponent (3) (Normal.Realtime.RealtimeView)"

The connection doesn't disconnect, despite the first error saying that its disconnecting. The connection status is "Ready", so scripts that want to use the realtime connection can't tell that there has been an error.

## Repro

The SampleScene contains 10x LargeComponent scene view instances. Each model has a byte array initialized with a size of 128x128x3.

In our original project local players spawn their custom avatar when didConnectToRoom is triggered. The last log message that's received is when the player voice is initializing. In this minimal repro, the didConnectToRoom event doesn't seem to be being triggered, so I added a "Spawn Avatar" context menu item. It can be found on the AvatarSpawner object in the scene, in the BasicAvatarSpawner script's hamburger menu.

Clicking SpawnAvatar when the game is running and after the errors have been logged to the console will cause the editor to freeze instantly. Note that the CPU usage is 0%, so it seems to be some kind of deadlock rather than an infinite loop.

While creating this repro project, i also found another way to cause the same freeze: Enter play mode, wait for the errors to be logged, and then exit play mode. The game will freeze in the same way, even though the avatar was never spawned.
