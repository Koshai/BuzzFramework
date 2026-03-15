window.buzzTheme = (function () {
    const storageKey = "buzz-active-theme";
    const defaultTheme = "azure";

    function normalize(theme) {
        if (typeof theme !== "string") {
            return defaultTheme;
        }

        const value = theme.trim().toLowerCase();
        if (!value) {
            return defaultTheme;
        }

        return value;
    }

    function apply(theme) {
        const normalized = normalize(theme);
        document.documentElement.setAttribute("data-buzz-theme", normalized);
        try {
            localStorage.setItem(storageKey, normalized);
        } catch {
            // Ignore storage issues in restricted browsing modes.
        }

        return normalized;
    }

    function getStoredTheme() {
        try {
            return normalize(localStorage.getItem(storageKey) || defaultTheme);
        } catch {
            return defaultTheme;
        }
    }

    return {
        apply,
        getStoredTheme
    };
})();
