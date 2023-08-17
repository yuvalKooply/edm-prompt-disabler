# edm-prompt-disabler
Disables the annoying EDM prompt message

## Installation
Clone the project, for example via:

`git clone git@github.com:yuvalKooply/edm-prompt-disabler.git /Users/yuval/Development/Apps/edm-prompt-disabler`

Create a symlink into your project's Editor/_private folder (It should be within an Editor folder and _private means it'll be ignored by git):
### MacOS / Linux
 `ln -s /Users/yuval/Development/Apps/edm-prompt-disabler /Users/yuval/Development/Apps/g-01/Assets/Editor/_private`
### Windows
I've never tried to run it on Windows but if you want to try it should be something like this (Warning: try at your own risk!):
1. Launch cmd *as admin* (Start -> type 'cmd', right click 'Run as administrator')
2. cd into your g-01/Packages folder
3. Create the symlinks, for example:
`mklink /j %homedrive%%homepath%/Development/Apps/g-01/Assets/Editor/_private %homedrive%%homepath%/Development/Apps/edm-prompt-disabler`

Now open your project in Unity and navigate to "Assets/Editor/edm-prompt-disabler". Make sure you have the file "edm-prompt-disabler.asmdef" and open its inspector. Make sure it looks like this:

<img width="397" alt="Screenshot 2023-08-17 at 12 40 58 PM" src="https://github.com/yuvalKooply/edm-prompt-disabler/assets/97828418/286af3a4-ad1f-4c62-8896-ff7b5ed3597a">

and click the "Apply" button below if you changed something.
That's it!

## But how does it work?
It adds the following line to "ProjectSettings/GvhProjectSettings.xml":

`<projectSetting name="GooglePlayServices.PromptBeforeAutoResolution" value="False" />`

And closes any window titled: "Enable Android Auto-resolution?"

When:
* You launch Unity
* You enter/exit play mode (delayed by a few seconds)
* Unity reloads your scripts

It never showed any signs of slowdown on my Mac for months.

## Known Bugs & Issues:
It seems to work 99% of the time but you have to make sure GvhProjectSettings.xml always have that PromptBeforeAutoResolution line in the git repo, or you'll see the message whenever Unity starts and when you switch branches.
If you still happen to see the prompt - use "Kooply/Editor/Disable EDM Prompt" to close it and disable it in the future. It's unlikely to popup again.

You might occusionally see a "Failed to disable prompt" warning in the Console tab. You can ignore it unless it happens too often.
