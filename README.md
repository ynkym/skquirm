# Skquirm!
CSC404 / GDES3064 Project\
East Wind Zaibatsu (Team 4)

### Design Doc
 - [Game Idea Pitch][]
 - [GDD Presentation][]
 - [Game Design Doc][]

### Unity Project
 - Notify other people if you are making changes to ***scenes*** or ***prefabs***, as they do not merge very nicely.
 - Controls:
   - left, right, up, down -> move the boat
 - Vortex (Whirlpool):
   - **ver.0.1**: Probably needs some adjustments or changes to make it feel nicer.
   - The script is attached to the "Cube" object (the boat) currently.\
   Add the script to objects that you want to be affected by the whirlpool force. Requires RigidBody.
   - buoyancyStrength ... strength of buoyancy. Makes the boat float up faster.
   - pullStrength ... strength of pull towards the center.
   - whirlStrength ... strength of the force that makes the boat go around in circle.

### Useful Links
 - [U of T Course Website][CSC404 Website]



[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [Game Idea Pitch]: <https://docs.google.com/presentation/d/1by40Y_oPAEWIIuCWlgDTlUJK4PtIrcA-H8tkTagFU3g/edit>
   [GDD Presentation]: <https://docs.google.com/presentation/d/1KbRS-JjiGd4jQM3KY-zrih-xLPNXNLOLOrOjMXOgQNE/edit>
   [Game Design Doc]: <https://docs.google.com/document/d/1uv2_NcELJFuv6RzsPlApDkYNizlAk4uU52X1iK6XKFQ/edit>
   [CSC404 Website]: <http://www.cdf.utoronto.ca/~csc404h/winter/>