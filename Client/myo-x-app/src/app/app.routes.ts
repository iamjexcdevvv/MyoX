import { Routes } from "@angular/router";

export const routes: Routes = [
	{
		path: "auth",
		children: [
			{
				path: "register",
				loadComponent: () =>
					import("./features/auth/register/register.component").then(
						(c) => c.RegisterComponent
					),
			},
		],
	},
];
