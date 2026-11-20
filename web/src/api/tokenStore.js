const TOKEN_KEY = 'token'

function readStoredToken() {
  try {
    return localStorage.getItem(TOKEN_KEY)
  } catch {
    return null
  }
}

let cachedToken = readStoredToken()

export function getToken() {
  return cachedToken
}

export function setToken(token) {
  cachedToken = token
  try {
    localStorage.setItem(TOKEN_KEY, token)
  } catch {
    // storage unavailable (private mode); token stays in memory
  }
}

export function clearToken() {
  cachedToken = null
  try {
    localStorage.removeItem(TOKEN_KEY)
  } catch {
    // storage unavailable; nothing to clear
  }
}