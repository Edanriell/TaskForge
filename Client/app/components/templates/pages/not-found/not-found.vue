<script lang="ts" setup>
	import { Button } from "@atoms";

	const handleGoHome = () => {
		navigateTo("/");
	};

	const handleGoBack = () => {
		const router = useRouter();
		router.back();
	};
</script>

<template>
	<div class="flex min-h-screen items-center justify-center bg-base-200">
		<div class="text-center">
			<div class="relative mb-10">
				<h1
					class="text-[10rem] font-black leading-none tracking-tighter text-primary opacity-10 select-none"
				>
					404
				</h1>
			</div>
			<h2 class="mb-4 text-3xl font-bold">Oops! Page not found</h2>
			<p class="text-base-content/70 mb-10 text-lg">
				The page you're looking for doesn't exist or has been moved.
			</p>
			<div class="flex justify-center gap-4">
				<Button color="blue" classes="btn-lg gap-2" @click="handleGoHome">
					<Icon name="heroicons:home" class="h-5 w-5" />
					Go Home
				</Button>
				<Button color="neutral" classes="btn-lg gap-2" outline @click="handleGoBack">
					<Icon name="heroicons:arrow-left" class="h-5 w-5" />
					Go Back
				</Button>
			</div>
		</div>
	</div>
</template>
