window.buzzGuideEnhanceCodeBlocks = function (containerSelector) {
    const root = document.querySelector(containerSelector);
    if (!root) {
        return;
    }

    const blocks = root.querySelectorAll("pre");
    for (const pre of blocks) {
        if (pre.dataset.copyEnhanced === "true") {
            continue;
        }

        pre.dataset.copyEnhanced = "true";
        pre.classList.add("guide-pre-copy");

        const button = document.createElement("button");
        button.type = "button";
        button.className = "guide-copy-button";
        button.textContent = "Copy";

        button.addEventListener("click", async () => {
            const code = pre.querySelector("code");
            const text = (code?.innerText ?? pre.innerText ?? "").trim();
            if (!text) {
                return;
            }

            try {
                await navigator.clipboard.writeText(text);
                const prior = button.textContent;
                button.textContent = "Copied";
                setTimeout(() => {
                    button.textContent = prior ?? "Copy";
                }, 1400);
            } catch {
                button.textContent = "Copy failed";
                setTimeout(() => {
                    button.textContent = "Copy";
                }, 1400);
            }
        });

        pre.appendChild(button);
    }
};
