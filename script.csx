public class Script : ScriptBase {
	public override async Task<HttpResponseMessage> ExecuteAsync() {
		var overrideCheck = this.Context.Request.Headers.TryGetValues("x-api-endpoint", out var varApiEndpoint);
		string apiEndpoint = String.Concat(varApiEndpoint);
		if(overrideCheck) {
			if(!apiEndpoint.StartsWith("/")) {
				apiEndpoint = String.Concat("/", apiEndpoint);
			}
			this.Context.Request.RequestUri = new Uri(String.Concat(this.Context.Request.RequestUri, apiEndpoint));
		}
		HttpResponseMessage response = await this.Context.SendAsync(this.Context.Request, this.CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		return response;
	}
}