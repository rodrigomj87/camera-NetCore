let _storage = window.localStorage;

function _get(key, isSession) {
    _setStorage(isSession);
    return _storage.getItem(key);
}

function _add(key, data, isSession) {
    _setStorage(isSession);
    _storage.setItem(key, data);
}

function _update(key, data, isSession) {
    _setStorage(isSession);
    _storage.setItem(key, data);
}

function _remove(key, isSession) {
    _setStorage(isSession);
    _storage.removeItem(key);
}

function _reset(isSession) {
    _setStorage(isSession);
    _storage.clear();
}

function _setStorage(isSession) {
    if (isSession) _storage = window.sessionStorage;
    else _storage = window.localStorage;
}

export default {
    get: _get,
    add: _add,
    update: _update,
    remove: _remove,
    reset: _reset,
}
