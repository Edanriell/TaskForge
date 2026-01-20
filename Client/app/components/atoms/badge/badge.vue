<script lang="ts" setup>
	interface Props {
		variant?: "success" | "warning" | "neutral" | "error" | "info" | "primary";
		icon?: string;
		size?: "xs" | "sm" | "md";
	}

	const props = withDefaults(defineProps<Props>(), {
		variant: "neutral",
		size: "sm"
	});

	const variantClass = computed(() => `badge-${props.variant}`);
</script>

<template>
	<span :class="['badge gap-1', `badge-${size}`, variantClass]">
		<Icon v-if="icon" :name="icon" class="h-3 w-3" />
		<slot />
	</span>
</template>
