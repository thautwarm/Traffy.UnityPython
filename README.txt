UnityPython
------------

UnityPython is a Python implementation which runs everywhere through IL2CPP, making it a good option for modding games.

UnityPython is based on CPython 3.10, but not fully compatible with CPython.

Usage (CLI)
------------

1. runtime: Download the binaries from the release page, and unpack them to a directory in '$PATH'.
2. compiler: 'pip install upycc' with a Python (>=3.8) distribution.
3. create a project as follow, 'cd' into it, and run this project with 'upy .'

    - myupy_proj
        - main.py
            from .libs.some_lib import lib_func
            args, kwargs = lib_func(1, 2, 3, a=1, b=2)
            print(args, kwargs)

        - libs
            - some_lib.py
                def lib_func(*args, **kwargs):
                    return args, kwargs

        - out (for compiler output)

Usage (Unity Library)
----------------------

Under construction.

IDE Support
-----------------

UnityPython has excellent IDE support based on VSCode Pylance:

    https://github.com/thautwarm/vscode-unitypython

Install 'VSCode-UnityPython' in VSCode, and use a Python environment with 'upycc' package installed is sufficient.
The UnityPython code will be compiled on-the-fly when you edit, and your game can set up a file watcher to hot reload everything.

Development
--------------------

Check out DEV.md in this project.
