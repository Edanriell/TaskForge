<script lang="ts" setup>
	interface Props {
		value: number;
		max?: number;
		variant?: "primary" | "success" | "warning" | "error";
	}

	const props = withDefaults(defineProps<Props>(), {
		max: 100,
		variant: "primary"
	});
</script>

<template>
	<progress :value="value" :max="max" :class="['progress h-2 w-full', `progress-${variant}`]" />
</template>
