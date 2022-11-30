import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ViviendaUpdateFormComponent } from './vivienda-update-form.component';


describe('ViviendaUpdateFormComponent', () => {
  let component: ViviendaUpdateFormComponent;
  let fixture: ComponentFixture<ViviendaUpdateFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ViviendaUpdateFormComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViviendaUpdateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
