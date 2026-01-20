<script lang="ts" setup>
	interface Props {
		label: string;
		value: string | number;
		icon: string;
		iconColor?: string;
	}

	withDefaults(defineProps<Props>(), {
		iconColor: "text-primary"
	});
</script>

<template>
	<div class="card bg-base-100 border border-base-300 shadow-sm">
		<div class="card-body p-4">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-base-content/60 text-sm">{{ label }}</p>
					<p class="text-2xl font-bold">{{ value }}</p>
				</div>
				<div class="rounded-xl bg-base-200 p-3">
					<Icon :name="icon" :class="['h-6 w-6', iconColor]" />
				</div>
			</div>
		</div>
	</div>
</template>
