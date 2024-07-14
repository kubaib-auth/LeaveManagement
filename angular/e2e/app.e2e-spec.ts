import { ManagementSystemTemplatePage } from './app.po';

describe('ManagementSystem App', function() {
  let page: ManagementSystemTemplatePage;

  beforeEach(() => {
    page = new ManagementSystemTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
