import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.scss']
})
export class SearchbarComponent  {

  @Input()
  public value = ''

  @Input()
  public placeholder = ''

  constructor(private router: Router) { }

  search(): void {
    this.router.navigate(['/search'], { queryParams: {q: this.value}})
  }
}
