import json
from abc import ABC, abstractmethod
from typing import final
print(json.JSON)

orig = "{\"sada\": [1, 2, 3, {}]}"
xs = json.loads('{"sada": [1, 2, 3, {}]}')
print(type(xs))
print(orig)



class S(ABC):
    @abstractmethod
    def f(self):
        raise NotImplementedError

expect = r"""{
    "sada" : [
        1,
        2,
        3,
        {}
    ]
}"""

assert iter(json.dumps(xs, indent = 4).splitlines()).map(str.strip).tolist() == iter(expect.splitlines()).map(str.strip).tolist()

def f():
    pass

# isinstance(1, json.JSON)