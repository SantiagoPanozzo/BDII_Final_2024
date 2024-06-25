import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PartidoPrediccionComponent } from './partido-prediccion.component';

describe('PartidoPrediccionComponent', () => {
  let component: PartidoPrediccionComponent;
  let fixture: ComponentFixture<PartidoPrediccionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PartidoPrediccionComponent]
    });
    fixture = TestBed.createComponent(PartidoPrediccionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
