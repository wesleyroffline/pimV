const API_BASE_URL = window.API_BASE_URL || "";

function getUser() {
    const raw = localStorage.getItem("user");
    if (!raw) {
        return null;
    }

    try {
        return JSON.parse(raw);
    } catch {
        clearSession();
        return null;
    }
}

function getToken() {
    return localStorage.getItem("token");
}

function getAuthHeaders() {
    const headers = {
        "Content-Type": "application/json"
    };

    const token = getToken();
    if (token) {
        headers.Authorization = `Bearer ${token}`;
    }

    return headers;
}

async function authorizedFetch(path, options = {}) {
    const requestHeaders = {
        ...getAuthHeaders(),
        ...(options.headers || {})
    };

    return fetch(`${API_BASE_URL}${path}`, {
        ...options,
        headers: requestHeaders
    });
}

function setSession(response) {
    localStorage.setItem("user", JSON.stringify(response.user));
    if (response.token) {
        localStorage.setItem("token", response.token);
    } else {
        localStorage.removeItem("token");
    }
}

function clearSession() {
    localStorage.removeItem("user");
    localStorage.removeItem("token");
}

function roleToPage(role) {
    if (role === "ADMIN") {
        return "admin.html";
    }

    if (role === "ALUNO") {
        return "aluno.html";
    }

    if (role === "PROFESSOR") {
        return "professor.html";
    }

    return "login.html";
}

function redirectByRole(role) {
    window.location.href = roleToPage(role);
}

async function authenticate(username, password) {
    const response = await fetch(`${API_BASE_URL}/api/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ username, password })
    });

    let data = null;
    try {
        data = await response.json();
    } catch {
        data = null;
    }

    if (!response.ok) {
        const message = data?.message || "Falha na autenticação.";
        throw new Error(message);
    }

    return data;
}

function requireAuthenticatedUser() {
    const user = getUser();

    if (!user) {
        window.location.href = "login.html";
        return null;
    }

    return user;
}

function protectRoute(allowedRoles = []) {
    const user = requireAuthenticatedUser();
    if (!user) {
        return null;
    }

    if (allowedRoles.length > 0 && !allowedRoles.includes(user.role)) {
        redirectByRole(user.role);
        return null;
    }

    return user;
}
