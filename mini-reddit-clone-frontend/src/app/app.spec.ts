import { TestBed } from '@angular/core/testing';
import { App } from './app';

describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Hello, mini-reddit-clone-frontend');
  });
});
// Function added: 1767982549
// New feature 1767983652
function add_esqqkr() {}
// Logic update: AIAlkzsyTGvV
// Logic update: rS9ppGT5x6rB
// Logic update: js1quwYq8RvW
// Logic update: 60Skxq2pisaL
// Logic update: lZUiauSkSHEB
