<script setup lang="ts">
	type InputTypes =
		| "text"
		| "password"
		| "email"
		| "number"
		| "tel"
		| "url"
		| "search"
		| "date"
		| "time"
		| "datetime-local"
		| "month"
		| "week"
		| "color"
		| "file"
		| "range"
		| "hidden"
		| "checkbox"
		| "radio";

	interface Props {
		placeholder?: string;
		type?: InputTypes;
	}

	const { placeholder = "", type = "text" } = defineProps<Props>();
</script>

<template>
	<input class="input input-bordered w-full mb-3" :placeholder="placeholder" :type="type" />
</template>

