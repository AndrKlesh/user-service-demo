import json
import sys

import requests

URL = "http://localhost:5000/api/users/register"
EXPECTED = ("id", "email")


def register(email: str, password: str) -> dict | None:
    payload = {"email": email, "password": password}
    print(f"POST {URL} {json.dumps(payload, ensure_ascii=False)}")

    try:
        resp = requests.post(URL, json=payload, timeout=5)
    except requests.ConnectionError:
        print("ERROR: backend недоступен (запустите 'backend: run')")
        return None
    except requests.Timeout:
        print("ERROR: таймаут запроса (5s)")
        return None

    print(f"<- {resp.status_code} {resp.text}")

    if resp.status_code >= 400:
        print(f"ERROR: backend вернул {resp.status_code}")
        return None

    try:
        data = resp.json()
    except ValueError:
        print("ERROR: ответ не JSON")
        return None

    missing = [f for f in EXPECTED if f not in data]
    if missing:
        print(f"ERROR: нет полей контракта v1: {missing} (backend перешёл на v2?)")
        return None

    extra = [f for f in data if f not in EXPECTED]
    if extra:
        print(f"ERROR: лишние поля вне контракта v1: {extra}")

    result = {f: data[f] for f in EXPECTED}
    print(f"OK: {result}")
    return result


if __name__ == "__main__":
    r = register("student@example.com", "password123")
    sys.exit(0 if r else 1)