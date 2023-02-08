# CMD-MacroRunner
Run apps from the Windows Command line using configerable macros

---

## Install
1. Downloading the Release.

    Go to the 'Release' tab and download the latest version of dShell.
    Save the .exe file in your Windows folder ('C:\Windows\System32') or any folder you can remember, like Documents.

2. Adding run.exe to PATH.

    Edit your PATH Enviroment Variable.
    Add a new path. Make sure you enter the full path including the dShell.exe (e.g: 'C:\Windows\System32\dShell.exe').

3. Restart your shell.

    Restart your command prompt or windows powershell window. This is to refresh the loaded Enviroment Variables.

4. First time setup

    Excecute the command 'run' without any other arguments to run the first time setup. The program will create a config in your Documents folder.
    ### Skipping this step and immediately registering or running a macro leads to an error. You do not have to reinstall the program, just type 'run' in your cmd again.
    
5. Enjoy!

    the tool is now set up. Excecute 'run -help' for instructions on how to register new programs, excecute your macros and more. The tool also works in your RUN dialogue (Win+R). Just type the command 'run <macro>' to run any software or file, without having to enter the full path or search it in the explorer.
