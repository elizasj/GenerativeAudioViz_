This workshop will take you through some concepts to know when getting started with the different software, and how to get everything to play nice together. The contents of this repo and the goal of this workshop is to give you a starting point. The information presented here is by no means exhaustive, but I've included links to sources and resources for you to further  explore the ideas presented. Documentation The scripts included in the Unity project are also highly commented and I highly encourage you to spend some time 

- [Unity Basics](#unity-basics)
- [Orca Basics](#orca-basics)
- [Live Basics](#live-basics)
- [Orca + Unity](#orca-+-unity)
- [Bonus Concepts](#bonus-concepts)
- [Conclusion](#conclusion)

### Unity Basics

Create a Cube [GameObject](https://docs.unity3d.com/560/Documentation/Manual/class-GameObject.html) and add the _[Rotate.cs](https://github.com/elizasj/GenerativeAudioViz_/blob/master/Assets/Scripts/Rotate.cs)_ component. 

⚗️Play with different [Vector3 properties](https://docs.unity3d.com/ScriptReference/Vector3.html) to develop your intuition about how things direct themselves and move around in 3D space.  
- x, _left-right_
- y,  _up-down_
- z, _front-back_

Rotation happens on the axis of whatever property you choose.

You'll notice that in the script, we are calling ```.Rotate```  on the transform - this is another important concept in Unity. The [Transform](https://docs.unity3d.com/ScriptReference/Transform.html) of any GameObject stores its position, rotation and scale. 

Other handy concepts to understand when getting started with Unity include [meshes](https://docs.unity3d.com/Manual/AnatomyofaMesh.html), which are basically the skeleton body of any 3D shape and [materials](https://docs.unity3d.com/Manual/Materials.html), which can be thought of like the skin that sits on top of the skeleton (Mesh,) of any 3D shape.
    
### Orca Basics
Orca is a language comprised of [26 functions](https://github.com/hundredrabbits/Orca), one for every letter of the alphabet. It's also a live coding environment, inside which you can use your arrow keys to move around. 
    
-  Arrows + `ctrl`  to jump across cells
-   `Shift` + `arrows` to select blocks
-   Drag blocks of highlighted code around by holding down `alt` 
-   `Ctrl` + `i` for insert mode to type normally

Add a  `D` (_Delay_) to your environment and you'll see a flashing star, `*`, appearing at regular intervals right underneath the letter. This is called a __bang__, and is a core concept in Orca - everything you do in Orca is at the service of creating bangs. Which represent the sequences and patterns we'll be making as they are sent out from Orca toward other software set up to receive them (Live or Unity for example.) 

The `D` we just set up will flash a bang every 8 frames unless changed through _mod_, the optional property to the right of the `D`. Notice there is also a parameter to the left. If you place the cursor on top of this parameter, you'll see a hint about what it does appear at the bottom left corner of your Orca window. Play with adding numbers to either side of `D` to understand how to speed up and slow down your bang.
    
In order to interface with other software, we'll need to assign a data format to our bangs. As mentioned, we'll be working with MIDI, which Orca sends using the colon symbol `:` It comes with five parameters - channel, octave, note, velocity & length. You'll need the first three parameters to make any kind of sound or visual, the last two can be left empty.

An example of this would be:

![](https://github.com/elizasj/GenerativeAudioViz_/blob/master/Assets/Static/orcademo.gif)

A brief overview of the MIDI parameters:  __octave__ and __note__ refer to the pitch of a sound, while __velocity__ has to do with the loudness/quietness of said pitch. __Length__ is how long the pitch holds before petering out. You can play with these four elements to make some pretty interesting sound and visual variations.

⚠️ The first parameter, __channel__, represents the address we're sending our bang to be interpreted in other software, which in our case is Live and Unity.

Now that you know how to create and send a bang, I suggest playing around with how you might combine any of the following: 

-   `R` _Random_, generates a random value
-   `C` _Count_, same idea as D but only counts, no bang 
-   `B`_Bounce_, like Count but goes up and down/back and forth
-   `T` _Track_, creates sequences of notes to iterate through
-   `V` _Variable_, stores values and use elsewhere on the grid
-   `K` _Konkat_, outputs a bunch of variables at once

At any point, hit CMD + G and Orca's documentation will appear in the window. Yes, the docs are sparse. All the more reason to experiment, play and see what happens! 

Remember to place your cursor over the white and green dots that appear around the letters to see what the parameters are for. 

### Ableton Live Basics
>If you're on a PC, make sure you've got LoopMidi already running for this next part. 

With Live open in Session View, where all tracks are organised into columns, you can begin setting up the audio samples and effects you want to use. Each column also has a bottom zone with the header __Midi From__. This is where you set your driver. On a Mac you'll set _IAC Bus_, on a PC you'll set _LoopMidi_. Then you choose the channel you want to receive on this track, incoming from Orca.

⚠️ Where Live counts up from _Channel 1_, Orca counts up from _Channel 0_. So keep that in mind when you're jumping between software. Live _Channel 1_ == Orca _Channel 0_ ...etc.

Now it's time to get curious and creative with sound. Live has a lot to offer in terms of instruments and effects, if you don't yet have your own to play with. Live's integrated samples can be drag-&-dropped onto any MIDI column. For audio samples, you'll need to first create a Simpler instrument, then drag and drop audio effects on top of the instrument, (not the column, but the instrument just below where it says _Drop Sample Here_.) To add Midi or Audio Effects do the same:
#### adding a midi sample    
![](https://github.com/elizasj/GenerativeAudioViz_/blob/master/Assets/Static/midisample.gif)

#### adding an audio sample with Simpler sampler
![](https://github.com/elizasj/GenerativeAudioViz_/blob/master/Assets/Static/simplersampling.gif)

#### adding MIDI and audio effects to samples
![](https://github.com/elizasj/GenerativeAudioViz_/blob/master/Assets/Static/addingfx.gif)

### Orca  + Unity
Now that we've got our bangs generating some sounds in Live, let's get some visuals going too.  We'll use Keijiros very handy [MidiJack package](https://github.com/keijiro/MidiJack) to bring MIDI into Unity where we'll be able to outline in code how and what we want to be generated. 

Positioning large amounts of objects accurately and beautifully in 3D space is hard, so we're going to lean on an algorithm to do the heavy lifting for us, and tweak from there. If you're not familiar with the Golden Ratio, check out [this video](https://www.youtube.com/watch?v=KWoJgHFYWxY&t=196s) for a quick rundown of the math involved in the _[PhyloPatternGen.cs](https://github.com/elizasj/GenerativeAudioViz_/blob/master/Assets/Scripts/PhyloPatternGen.cs)_  script. I've also included lots of comments in the script itself to help you develop your understanding of some handy Unity methods like `Instantiate()`, `parent`, `.lookAt()` and `setActive()` which you'll find yourself using again and again when creating with Unity.

Attach this script to a new empty GameObject, which you'll rename _Generator_. Create a cube and deactivate it by removing the tick from the box in it's inspector.  We're deactivating it here because our script will flag any generated cubes to active when we run our scene. This way we start with a blank canvas, and our scene doesn't have a random cube sitting in the middle of our creation. 

![](https://github.com/elizasj/GenerativeAudioViz_/blob/master/Assets/Static/Screenshot%202019-11-18%20at%2020.07.29.png?raw=true)

In the Hierarchy tab, grab the cube and drab it to the _Generator_'s inspector, drop it in the Cube slot of the _PhyloGenPattern_ component and hit the play button. You'll see as many cubes as there are MIDI notes (128) spring to life in your Game View window. 

![](https://raw.githubusercontent.com/elizasj/GenerativeAudioViz_/master/Assets/Static/phygen.gif)

This happens because each cube generated by our script has _another_ script attached to it, called _NoteIndicator.cs_, which allocates a number from 0 to 128 to each cube, representing a MIDI note number. Whenever you play a note in Orca, a cube with scale up. 

⚗️Try playing around with velocity and length in Orca and see what happens to you cube. What happens if you change the octave?

Just as before, this script is also full of comments and hints about the code. Take some time to look at how the different MIDI channels are targeted. Note that while you must always include the variable noteNumber in the function call, you can omit the channel number. 

For example:

`MidiMaster.GetKey(MidiChannel.Ch1, noteNumber)`  will grab data for any note being played on Channel 1.

`MidiMaster.GetKey(noteNumber)`will grab data for any note being played on _any_ channel. You can achieve the same result with `MidiMaster.GetKey(MidiChannel.All, noteNumber)` 

### Bonus Concepts
Creative coders love simple, smooth animations. I've included some of the communities historically favorite go-to's, __Lerp__ and __Perlin Noise__ for you to experiment with.

Think of [__Perlin Noise__](https://en.wikipedia.org/wiki/Perlin_noise) as poetic randomness. When used in the context of animation it adds a life-like quality of movement to objects. In our case, it produces the initial waviness of the cubes. This way even if nothing is firing in Orca, the scene feels alive.

__Lerp__ is short for [_Linear Interpolation_](https://en.wikipedia.org/wiki/Linear_interpolation).  Think of Lerp as a percentage of a space between two markers. In the scripts folder, I've included a stand-alone script called Lerp. To develop your intuition around this idea, add the script to an empty GameObject, hit play and fiddle with the `T` property in the inspector. Lerp is really handy for things like mixing colors and animating objects.

### Conclusion

You know have all the elements you need to get started making interesting visual and auditory experiments. Once you've created a visual playground you're satisfied with, you can build your Unity project into an app and perform it with Orca in real-time.
