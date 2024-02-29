import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: { "content-type": "application/json" },
    method: "POST",
  };
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/user/forget_password?email=${requestBody}`,
    options,
  );
  return new Response(JSON.stringify(body.ok), { status: body.status });
};
