import { Component } from '@angular/core';
import { navItems } from '../menu';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent {
  public navItems = navItems;

}
