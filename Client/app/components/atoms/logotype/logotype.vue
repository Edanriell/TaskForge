<script lang="ts" setup>
	interface Props {
		classes?: string;
	}

	const { classes = "" } = defineProps<Props>();
	const attrs = useAttrs();
</script>

<template>
	<h1 
		:bind="attrs"
		:class="`ml-4 items-center gap-2 text-primary text-2xl font-extrabold tracking-tight ${classes}`">
		<Icon name="mdi:hammer-wrench" class="h-6 w-6" />
		TaskForge
	</h1>
</template>
