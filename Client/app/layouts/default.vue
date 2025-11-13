<script lang="ts" setup>
	import { Header, Sidebar } from "@organisms";
</script>

<template>
	<div class="bg-base-100 w-full grid lg:grid-cols-[auto_1fr]">
		<Header />
		<Sidebar />
		<main class="lg:col-start-2">
			<slot />
		</main> 
	</div>
</template>
