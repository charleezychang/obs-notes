#### Creating standalone components:
```ts
@Component({
    standalone: true,
    selector: 'photo-gallery',
    imports: [ImageGridComponent],
    template: `
        ... <image-grid [images]="imageList"></image-grid>
    `,
}) 

export class PhotoGalleryComponent {
     // component logic 
}
```
- can import modules and can be imported in modules