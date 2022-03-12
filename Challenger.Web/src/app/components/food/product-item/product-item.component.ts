import { Component, Input, OnInit } from '@angular/core';
import { ProductDto } from '../product-add/ProductDto';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent implements OnInit {

  @Input() product: ProductDto;

  constructor() { }

  ngOnInit(): void {
  }

}
