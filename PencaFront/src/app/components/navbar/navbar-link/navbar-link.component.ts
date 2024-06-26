import {booleanAttribute, Component, Input, OnInit} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-navbar-link',
  templateUrl: './navbar-link.component.html',
  styleUrls: ['./navbar-link.component.css']
})
export class NavbarLinkComponent implements OnInit {
  constructor(
      private router: Router
  ) {}

  activated: boolean = false;

  @Input()
  public url: string | null = null;

  @Input()
  public label: string | null = null;

  @Input({transform: booleanAttribute})
  public absolute: boolean = false;

  ngOnInit() {
    const route = this.router.url;
    if(this.absolute)
      this.activated = this.url == route;
    else {
      this.activated = route.startsWith(this.url!);
    }
  }
}
