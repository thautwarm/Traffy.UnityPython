import json

print(json.JSON)

orig = '{"sada": [1, 2, 3, {}]}'
xs = json.loads('{"sada": [1, 2, 3, {}]}')

assert isinstance("", json.JSON)
assert isinstance({}, json.JSON)
assert isinstance(1, json.JSON)
assert isinstance(True, json.JSON)
assert isinstance(1.0, json.JSON)
assert isinstance(None, json.JSON)
assert isinstance([], json.JSON)


expect = r"""{
    "sada" : [
        1,
        2,
        3,
        {}
    ]
}"""

assert (
    iter(json.dumps(xs, indent=4).splitlines()).map(str.strip).tolist()
    == iter(expect.splitlines()).map(str.strip).tolist()
)
