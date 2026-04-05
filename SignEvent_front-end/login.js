const form = document.getElementById("loginForm");
const usernameInput = document.getElementById("username");
const passwordInput = document.getElementById("password");
const errorMessage = document.getElementById("errorMessage");
const submitButton = document.getElementById("submitButton");

const currentUser = getUser();
if (currentUser) {
    redirectByRole(currentUser.role);
}

form.addEventListener("submit", async (event) => {
    event.preventDefault();
    errorMessage.textContent = "";

    submitButton.disabled = true;
    submitButton.textContent = "Entrando...";

    try {
        const response = await authenticate(usernameInput.value, passwordInput.value);

        if (response?.success === true && response?.user?.role) {
            setSession(response);
            redirectByRole(response.user.role);
            return;
        }

        errorMessage.textContent = "Credenciais inválidas.";
    } catch {
        errorMessage.textContent = "Credenciais inválidas.";
    } finally {
        submitButton.disabled = false;
        submitButton.textContent = "Entrar";
    }
});
