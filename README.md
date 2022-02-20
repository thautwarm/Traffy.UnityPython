## unitypython

Key points:

1. Run a Python-like programming language on ALL platforms such as iOS, Android, etc.
2. Good IDE support via Pylance (see `traffy-types/`).
3. Dynamic and fast code loading in Unity. Edit code and see how UI changes immediately (Unity Editor is painful to me)!
4. An extensible coroutine implementation. `async` and `await` are not as restricted as that in CPython and can be used as fast/fine-grained controlled event loops in game development.

Unity Python is not fully compatible to CPython. For instance, exceptions are heavy and not used for control flow.
