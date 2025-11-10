<script lang="ts" setup>
	import { Sidebar } from "@organisms";
</script>

<template>
	<div class="bg-base-100 w-full grid grid-cols-[auto_1fr]">
		<Sidebar />
		<main class="col-start-[2]">
			<slot />
		</main>
	</div>
</template>
