# ReMod.Core Quest Version
This is a port of the ReMod.Core project, originally ported as a ButtonAPI by XOX-Toxic with a restoration of it's function as the ReMod.Core by uwuclara and includes extended functionality by me, Cyril.

### ReMod.Core-Updater
You can use ReMod.Core-Updater plugin to automatically update ReMod.Core Quest Library that can be found [here](https://github.com/uwuclara/ReMod.Core-Updater).

# ReMod.Core

![ReMod Core Logo](https://github.com/RequiDev/ReMod.Core/raw/master/remod_core_logo.png)



## DISCLAIMER! THIS IS NOT A MOD. THIS IS JUST A DEPENDENCY FOR MODS!

## Description
This is a library capable of powering mods for the quest version of VRC. It will be used to power my own private and public mods, as well as an upcoming recreation of ReMod CE for quest

## Usage
If you don't intend to modify the project your best bet is to either download the latest release from [here](https://github.com/Cyril-Xd/ReMod.Core-Quest/releases/latest) 
If you **NEED** to modify ReMod.Core and need those changes to be in here, I'd suggest adding this repository to yours as a [git submodule](https://git-scm.com/book/en/v2/Git-Tools-Submodules).
Modify ReMod.Core as you need it, test your changes and create a pull request to submit your changes.

## Documentation
Read the code in [ReModCE](https://github.com/RequiDev/ReModCE). Requi wrote this library to use it for their projects. They're fine with other people using it as long as they respect the [license](https://github.com/RequiDev/ReMod.Core/blob/master/LICENSE) behind it.  


## Contribution
In case you do have something to contribute, please create a pull request. Try to stay close to the current coding style.

## How To Use
```
using System;
using System.Collections;
using MelonLoader;
using UnityEngine;
using UnhollowerRuntimeLib;
using ReMod.Core.Managers;
using ReMod.Core.Unity;
using ReMod.Core.VRChat;

internal static GameObject UserInterface;
internal static UiManager _uiManager;

public override void OnApplicationStart()
{
    MenuEx.Patch();
    
    ClassInjector.RegisterTypeInIl2Cpp<EnableDisableListener>();
    MelonCoroutines.Start(WaitForUI());
    
    IEnumerator WaitForUI()
    {
        yield return MenuEx.WaitForUInPatch();
        UserInterface = MenuEx.userInterface;
        OnMenuStart();
    }
}     

internal static OnMenuStart()
{
    _uiManager = new UiManager(menuName: "NameOfTheMenu", menuSprite: null);
}
```
## Credits
[@Kiokuu](https://github.com/Kiokuu)  
[@imxLucid](https://github.com/imxLucid)  
[@DDAkebono](https://github.com/DDAkebono)  
[@MintLily](https://github.com/MintLily)  
[@Kiokuu](https://github.com/Kiokuu)   
[@WTFBlaze](https://github.com/WTFBlaze)  
[@XOX-Toxic](https://github.com/toxicstuff)  
[@Cyril-XD](https://github.com/Cyril-Xd)  
[@uwuclara](https://github.com/uwuclara)   
