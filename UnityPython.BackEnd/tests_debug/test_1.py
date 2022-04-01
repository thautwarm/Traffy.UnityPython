import json
print(json.JSON)

orig = "{\"sada\": [1, 2, 3, {}]}"
xs = json.loads('{"sada": [1, 2, 3, {}]}')
print(type(xs))
print(orig)



expect = r"""{
    "sada" : [
        1,
        2,
        3,
        {}
    ]
}"""

assert iter(json.dumps(xs, indent = 4).splitlines()).map(str.strip).tolist() == iter(expect.splitlines()).map(str.strip).tolist()