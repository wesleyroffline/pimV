const pageRole = document.body.dataset.role;
const user = protectRoute([pageRole]);

if (user) {
    const usernameElement = document.getElementById("usernameDisplay");
    const roleElement = document.getElementById("roleDisplay");
    const nomeVisivel = user.username || user.nome || "Usuário";

    if (usernameElement) {
        usernameElement.textContent = nomeVisivel;
    }

    if (roleElement) {
        roleElement.textContent = user.role;
    }
}

const logoutButton = document.getElementById("logoutButton");
if (logoutButton) {
    logoutButton.addEventListener("click", () => {
        clearSession();
        window.location.href = "/login.html";
    });
}
