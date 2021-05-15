import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  private readonly BACKGROUND_COLORS = ['#080A17', '#021418', '#121F2F', '#171F22', '#191919', '#04101C', '#1D2528']
  public backgroundColor?: String;
  public index?: number;

  constructor() { }

  ngOnInit(): void {
    this.index = Math.floor(Math.random() * 7)
    this.backgroundColor = this.BACKGROUND_COLORS[this.index]
  }
}
