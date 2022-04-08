layout = 0


class testsuite:
    def __init__(self, name: str):
        self.name = name

    def __enter__(self):
        global layout
        layout += 1
        head = "----" * layout
        print(head + f"start testing: {self.name}")

    def __exit__(self, exc_type, exc_val, exc_tb):
        global layout
        head = "----" * layout
        if exc_type is None:
            print(head + f"test success: {self.name}")
        else:
            print(head + f"test failed: {self.name}")
        layout -= 1
