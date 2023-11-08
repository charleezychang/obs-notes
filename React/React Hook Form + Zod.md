[react-hook-form - npm (npmjs.com)](https://www.npmjs.com/package/react-hook-form)
[Home | React Hook Form - Simple React forms validation (react-hook-form.com)](https://www.react-hook-form.com/)
[React Hook Form (+ Zod & TypeScript & Server Errors) - COMPLETE Tutorial - YouTube](https://www.youtube.com/watch?v=u6PQ5xZAv7Q)

```tsx
import { useForm, FieldValues } from "react-hook-form"

export default function FormWithReactHookForm() {
	const {
		register,
		handleSubmit,
		formState: { errors, isSubmitting },
		reset,
		getValues 
	} = useForm()

	const onSubmit = async(data: FieldValues) {
		await new Promise(res => setTimeout(res, 1000))
		reset();
	}

	return (
		<form onSubmit={handleSubmit(onSubmit)}>
			<input
				{...register(
					"email", 
					{ required: "Email is required" }
				)}
			/>
			{errors.email && <p>{{errors.email.message}}</p>}
			<input
				{...register(
					"password",
					{ required: "Password is required", 
						minLength: {
							value: 10,
							message: "Password must be atleast 10 characters"
						}	
					}
				)}
			/>
			{errors.password && <p>{{errors.password.message}}</p>}
			<input
				{...register(
					"confirmPassword",
					{ required: "Confirm password is required",
						validate: (value) => {
							value === getValues("password") || "Password must match"
						}
					}
				)}
			/>
			{errors.confirmPassword && <p>{{errors.confirmPassword.message}}</p>}
		</form>
		<button>Submit</button>
	)
}
```

### Zod: A Form Schema
```tsx
// lib/types.ts
// npm i zod @hookform/resolvers
import { z } from "zod"

export const signUpSchema = z.object({
	email: z.string().email(),
	password: z.string().min(10, "Password must be atleast 10 characters"),
	confirmPassword: z.string()
})
.refine((data) => data.password === data.confirmPassword, {
	message: "Password must match",
	path: ["confirmPassword"]
})

export type TSignUpSchema = z.infer<typeof signUpSchema>;
```

```tsx
import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { TSignUpSchema, signUpSchema } from "zod"

const signUpSchema = z.object({
	email: z.string().email(),
	password: z.string().min(10, "Password must be atleast 10 characters"),
	confirmPassword: z.string()
})
.refine((data) => data.password === data.confirmPassword, {
	message: "Password must match",
	path: ["confirmPassword"]
})

type TSignUpSchema = z.infer<typeof signUpSchema>;

export default function FormWithReactHookForm() {
	const {
		register,
		handleSubmit,
		formState: { errors, isSubmitting },
		reset,
		setError
	} = useForm<TSignUpSchema>({
		resolver: zodResolver(signUpSchema)
	})

	const onSubmit = async(data: TSignUpSchema) {
		await fetch("/api/signup", {
			method: "POST",
			body: JSON.stringify(data),
			headers: {
				"Content-Type": "application/json"
			}
		})
		const responseData = await response.json();
		if (!response.ok) {
			alert("Submitting form failed!")
			return;
		}
		if (responseData.errors) {
			const errors = responseData.errors;
			if (errors.email) {
				setError("email", {
					type: "server",
					message: errors.email
				})
			}
			else if (errors.password) {
				setError("password", {
					type: "server",
					message: errors.password
				})
			}
			else if (errors.confirmPassword) {
				setError("confirmPassword", {
					type: "server",
					message: errors.confirmPassword
				})
			}
			else {
				alert("Something went wrong!")
			}
		}
		// reset();
	}

	return (
		<form onSubmit={handleSubmit(onSubmit)}>
			<input
				{...register("email")}
			/>
			{errors.email && <p>{{errors.email.message}}</p>}
			<input
				{...register("password")}
			/>
			{errors.password && <p>{{errors.password.message}}</p>}
			<input
				{...register("confirmPassword")}
			/>
			{errors.confirmPassword && <p>{{errors.confirmPassword.message}}</p>}
		</form>
		<button>Submit</button>
	)
}
```

```tsx
// app/api/signup
import { NextResponse } from "next/server";
import { signUpSchema } from "@/lib/types"

export async function POST(request: Request) {
	const body: unknown = await request.json();

	const result = signUpschema.safeParse(body)
	let zodErrors = {};
	if (!result.success) {
		result.error.issues.forEach((issue) => {
			zodErrors = { ...zodErrors, [issue.path[0]]: issue.message }
		})
	}

	return NextResponse.json(
		Object.keys(zodErrors).length > 0 
		? { errors: zodErrors }
		: { success: true }
	)
}
```