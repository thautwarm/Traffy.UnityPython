layout = 0
class testsuite:
    def __init__(self, name: str):
        self.name = name
        
    
    def __enter__(self):
        global layout
        layout += 1

    def __exit__(self, exc_type, exc_val, exc_tb):
        global layout
        head = '----' * layout
        if (exc_type is None):
            print(head + f"success: {self.name}");
        else:
            print(head + f"failed: {self.name}");
        layout -= 1
