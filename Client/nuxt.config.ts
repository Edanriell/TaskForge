// https://nuxt.com/docs/api/configuration/nuxt-config
import { fileURLToPath } from "url";

export default defineNuxtConfig({
	compatibilityDate: "2025-07-15",
	devtools: { enabled: true },
	alias: {
		"@atoms": fileURLToPath(new URL("./app/components/atoms", import.meta.url)),
		"@molecules": fileURLToPath(new URL("./app/components/molecules", import.meta.url)),
		"@organisms": fileURLToPath(new URL("./app/components/organisms", import.meta.url)),
		"@templates": fileURLToPath(new URL("./app/components/templates", import.meta.url)),
		"@layouts": fileURLToPath(new URL("./app/layouts", import.meta.url)),
		"@pages": fileURLToPath(new URL("./app/pages", import.meta.url))
	},

	modules: [
		"@nuxt/eslint",
		"@nuxt/image",
		"@nuxt/ui",
		"@nuxt/scripts"
	]
}) 