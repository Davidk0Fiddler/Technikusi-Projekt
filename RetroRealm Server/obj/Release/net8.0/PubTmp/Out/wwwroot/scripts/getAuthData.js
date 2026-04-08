export default async function getAuthData() {
    const sessionRefreshToken = sessionStorage.getItem("RefreshToken");
    const token = sessionStorage.getItem("Token");

    return {
        token: token,
        refreshToken: sessionRefreshToken,
    }
}