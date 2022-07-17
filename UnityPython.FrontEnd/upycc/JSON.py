try:
    import ujson

    def dump_json(x):
        return ujson.dumps(x, ensure_ascii=False, escape_forward_slashes=False)

except ImportError:
    import json

    def dump_json(x):
        return json.dumps(x, ensure_ascii=False)
