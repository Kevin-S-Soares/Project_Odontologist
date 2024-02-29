import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: { "content-type": "application/json" },
    body: JSON.stringify({
      hash: requestBody["hash"],
      userId: requestBody["userId"],
      operation: requestBody["operation"],
    }),
    method: "POST",
  };
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/user/verify_registration`,
    options,
  );
  return new Response(JSON.stringify(body.ok), { status: body.status });
};
