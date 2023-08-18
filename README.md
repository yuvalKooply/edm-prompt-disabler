# edm-prompt-disabler
Disables the annoying EDM prompt message

<img width="402" alt="Screenshot_2023-08-17_at_1_05_48_PM" src="https://github.com/yuvalKooply/edm-prompt-disabler/assets/97828418/d1687f33-e902-40a4-bdca-eaff0f8ca03c">

## Installation

You have 2 options:
1. Using Package Manager (easy but you can't edit the code)
2. Cloning

### 1. Using Package Manager

<img width="330" alt="Screenshot 2023-08-18 at 8 30 43 PM" src="https://github.com/yuvalKooply/edm-prompt-disabler/assets/97828418/62720933-647a-4e06-bb53-9586706a69aa">

In Unity, open Window/Package Manager and click the + button on the top left corner. Select "Add package from git URL..." and enter:

```
git@github.com:yuvalKooply/edm-prompt-disabler.git
```

In the text field. That's it!

### 2. Cloning

Clone the project, for example via:

```
git clone git@github.com:yuvalKooply/edm-prompt-disabler.git /Users/yuval/Development/Apps/edm-prompt-disabler
```

Make sure your project's .gitignore file contains these lines (g-01 does):
```
**/_private/
**/_private.meta
```

Create a symlink to your project's Editor/_private folder (It should be within an Editor folder and _private means it'll be ignored by git):
### MacOS / Linux
```
ln -s /Users/yuval/Development/Apps/edm-prompt-disabler /Users/yuval/Development/Apps/g-01/Assets/Editor/_private
```

### Windows
I've never tried to run it on Windows but if you want to try it should be something like this (Warning: try at your own risk!):
1. Launch cmd *as admin* (Start -> type 'cmd', right click 'Run as administrator')
2. cd into your g-01/Packages folder
3. Create the symlinks, for example:

```
mklink /j %homedrive%%homepath%/Development/Apps/g-01/Assets/Editor/_private %homedrive%%homepath%/Development/Apps/edm-prompt-disabler
```

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
* Unity reloads your scripts (delayed by a few seconds)

## Known Bugs & Issues:
It seems to work 99% of the time but you have to make sure GvhProjectSettings.xml always have that PromptBeforeAutoResolution line in the git repo, or you'll see the message whenever Unity starts and when you switch branches.
If you still happen to see the prompt - use "Kooply/Editor/Disable EDM Prompt" to close it and disable it in the future. It's unlikely to popup again.

You might occusionally see a "Failed to disable prompt" warning in the Console tab. You can ignore it unless it happens too often.
