### TanStack - React Query
[TanStack | High Quality Open-Source Software for Web Developers](https://tanstack.com/query/latest)
```tsx
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
const queryClient = newQueryClient()

ReactDOM.createRoot(document.getElementById("root")!).render(
	<React.StrictMode>
		<QueryClientProvider client={queryClient}>
			<App />
		</QueryClientProvider>
	</React.StrictMode>
)

const fetchJobItem = async (id: number): Promise<JobItemApiReponse> => {
	const response = await fetch(`url`)
	if (!response.ok) {
		const errorData = await.response.json()
		throw new Error(errorData.description)
		// take note this Error is an object (if its going to be handled in the onError)
		// access the string by error.message
	}
	
	const data = await response.json()
	return data
}

export function useJobItem(id: number | null) {
	const { data, isInitialLoading } = useQuery(
		// [queryKey(name), dependencies]
		['job-item', id],
		() => (id ? fetchJobItem(id) : null),
		// options
		{
			// staletime - duration of cache validity
			staleTime: 1000 * 60 * 60,
			refetchOnWindowFocus: false,
			retry: false,
			enabled: Boolean(id),
			onError: () => {}
		}
	)

	return {
		data.jobItem,
		isLoading: isInitialLoading
	} as const
}
```
### Vercel - SWR (state-while-revalidate)
[React Hooks for Data Fetching – SWR](https://swr.vercel.app/)