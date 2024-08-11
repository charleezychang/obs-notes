#### Handle responsiveness in component
```ts
isLeftSidebarCollapsed = signal<boolean>(false);
screenWidth = signal<number>(window.innerWidth)

@HostListener('window:resize')
onResize() {
	this.screenWidth.set(window.innerWidth);
	if (this.screenWidth() < 768) {
		this.isLeftSidebarCollapsed.set(true)
	}
}
```
