const API_BASE_URL = window.API_BASE_URL || "";

const APP_ROUTES = {
    login: "/login.html",
    admin: "/admin/",
    aluno: "/aluno/",
    professor: "/professor/"
};

function normalizeRole(role) {
    return (role || "").toString().trim().toUpperCase();
}

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
    const safeUser = {
        ...(response.user || {}),
        role: normalizeRole(response?.user?.role)
    };

    localStorage.setItem("user", JSON.stringify(safeUser));
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
    const normalizedRole = normalizeRole(role);

    if (normalizedRole === "ADMIN") {
        return APP_ROUTES.admin;
    }

    if (normalizedRole === "ALUNO") {
        return APP_ROUTES.aluno;
    }

    if (normalizedRole === "PROFESSOR") {
        return APP_ROUTES.professor;
    }

    return APP_ROUTES.login;
}

function redirectByRole(role) {
    const targetPage = roleToPage(role);
    const currentPath = window.location.pathname.endsWith("/")
        ? window.location.pathname
        : `${window.location.pathname}${window.location.pathname.endsWith(".html") ? "" : "/"}`;

    if (currentPath !== targetPage && window.location.pathname !== targetPage) {
        window.location.href = targetPage;
    }
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
        window.location.href = APP_ROUTES.login;
        return null;
    }

    return user;
}

function protectRoute(allowedRoles = []) {
    const user = requireAuthenticatedUser();
    if (!user) {
        return null;
    }

    const allowedNormalized = allowedRoles.map(normalizeRole);
    if (allowedNormalized.length > 0 && !allowedNormalized.includes(normalizeRole(user.role))) {
        redirectByRole(user.role);
        return null;
    }

    return user;
}
