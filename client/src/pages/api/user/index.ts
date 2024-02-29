import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: { "content-type": "application/json" },
    body: JSON.stringify({
      name: requestBody["name"],
      email: requestBody["email"],
      password: requestBody["password"],
    }),
    method: "POST",
  };
  const body = await fetch(`${process.env.SERVER ?? ""}/api/v1/user`, options);
  return new Response(JSON.stringify(body.ok ? await body.json() : null), {
    status: body.status,
  });
};
