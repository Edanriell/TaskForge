// https://nuxt.com/docs/api/configuration/nuxt-config
import tailwindcss from "@tailwindcss/vite";
import { fileURLToPath } from "url";

export default defineNuxtConfig({
	compatibilityDate: "2025-07-15",
	devtools: { enabled: true },
	css: ["~/assets/css/tailwind.css"],
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
		"@nuxt/fonts",
		"@nuxt/image",
		"@nuxt/scripts",
		"@nuxt/test-utils",
		[
			"@nuxtjs/stylelint-module",
			{
				configFile: ".stylelintrc.json"
			}
		],
		"motion-v/nuxt"
	],
	vite: { plugins: [tailwindcss()] }
});
